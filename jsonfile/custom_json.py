import os
import json


def custom_json(jsonfile):

    current_path = os.path.dirname(os.path.abspath(__file__))
    print(current_path)

    full_path = os.path.join(current_path, jsonfile)

    try:
        with open(full_path) as file:
            print(file)

            for line in file.readlines():
                print(line)

            file.close()

    except Exception as e:
        print(str(e))
        return e



custom_json(jsonfile="data.json")
