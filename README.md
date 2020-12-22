# Auto_Calibrator

>>> from openpyxl import load_workbook
>>> load_wb = load_workbook("D:/num.xlsx", data_only=True)
>>> load_ws = load_wb['Sheet1']
>>> all_values = []
>>> for row in load_ws.rows:
...     row_value = []
...     for cell in row:
...         row_value.append(cell.value)
...     all_values.append(row_value)
...
>>>
>>> print(all_values)
[[1, 10, 100, 1000, 10000, 100000], [2, 20, 200, 2000, 20000, 200000], [3, 30, 51470, 3000, 30000, 300000], [4, 40, 21470, 4000, 40000, 400000], [5, 50, 1470, 5000, 50000, 500000], [6, 60, 6470, 6000, 60000, 600000]]
>>>

