import csv
import pandas as pd

# def load_csv(file):
#     with open(file, 'r') as f:
#         reader = csv.reader(f)
#         return dict(reader)


def load_csv2(file):
    df = pd.read_csv(file)
    print(df['pre_dist'])
    return df



