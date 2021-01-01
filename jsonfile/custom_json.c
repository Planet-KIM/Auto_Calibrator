#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <stdbool.h>


typedef enum _TOKEN_TYPE {
	TOKEN_STRING,
	TOKEN_NUMBER,
} TOKEN_TYPE;

typedef struct _TOKEN {
	TOKEN_TYPE type;
	union {
		char *string;
		double *number;
	};
	bool isArray;
} TOKEN;

#define TOKEN_COUNT 40

typedef struct _JSON {
	TOKEN tokens[TOKEN_COUNT];
} JSON;


char *readFile(char *filename, int *readSize){

	FILE *fp = fopen(filename, "rb");
	if(fp == NULL)
		return NULL;

	int size;
	char *buffer;
	
	// file 크기 구하기
	fseek(fp, 0, SEEK_END);
	size = ftell(fp);
	fseek(fp, 0, SEEK_SET);

	// 파일 크기 + NULL 공간만큼 메모리 할당 후 0으로 초기화
	buffer = malloc(size + 1);
	memset(buffer, 0, size + 1);

	// 파일 내용 일기
	if(fread(buffer, size, 1, fp) < 1){
		*readSize = 0;
		free(buffer);
		fclose(fp);
		return NULL;
	}

	// 파일 크기 넘겨줌
	*readSize = size;
	
	fclose(fp);
	return buffer;
}

void parseJson(char *doc, int size, JSON *json){
	
	int tokenIndex = 0;
	int pos = 0;

	if (doc[pos] !=  '[')
		return;


	pos++;

	while(pos < size){
		switch(doc[pos])
		{
		case '"':
		{
			char *begin = doc + pos + 1;

			char *end = strchr(begin, '"');
			if(end == NULL)
				break;

			int stringLength = end - begin;

			json->tokens[tokenIndex].type = TOKEN_STRING;
			json->tokens[tokenIndex].string = malloc(stringLength + 1);
			memset(json->tokens[tokenIndex].string, 0, stringLength + 1);
			memcpy(json->tokens[tokenIndex].string, begin, stringLength);
			tokenIndex++;
			pos = pos + stringLength + 1; 
		}
		break;
		}
		pos++;
	}
}


void freeJson(JSON *json){
	
	for(int i=0; i < TOKEN_COUNT; i++){
		if(json->tokens[i].type == TOKEN_STRING)
			free(json->tokens[i].string);
	}
}


int main(){
	
	int size;

	char *doc = readFile("data.json", &size);
	if(doc == NULL)
		return -1;

	JSON json = { 0, };

	parseJson(doc, size, &json);

	/*
	printf("[0] : %s\n", json.tokens[0].string);
	printf("[1] : %s\n", json.tokens[1].string);
	printf("[2] : %s\n", json.tokens[2].string);
	printf("[3] : %s\n", json.tokens[3].string);
	
	printf("[4] : %s\n", json.tokens[4].string);
	printf("[5] : %s\n", json.tokens[5].string);
	printf("[6] : %s\n", json.tokens[6].string);
	printf("[7] : %s\n", json.tokens[7].string);

	printf("[8] : %s\n", json.tokens[8].string);
	printf("[9] : %s\n", json.tokens[9].string);
	printf("[10] : %s\n", json.tokens[10].string);
	printf("[11] : %s\n", json.tokens[11].string);
	
	printf("[12] : %s\n", json.tokens[12].string);
	printf("[13] : %s\n", json.tokens[13].string);
	printf("[14] : %s\n", json.tokens[14].string);
	printf("[15] : %s\n", json.tokens[15].string);*/

	for (int i=0; i < 33 ; i++){
		if (i % 4 == 0)
			printf("---------------------- %d Vesion --------------------\n", (i / 8)+1);
		
		if (json.tokens[i].string == NULL)
			break;
		
		printf("KEY : %s\t | VALUE : %s\n", json.tokens[i].string, json.tokens[i+1].string);

		i++;
	}

	freeJson(&json);

	free(doc);

	return 0;
}

