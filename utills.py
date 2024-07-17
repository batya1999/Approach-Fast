import csv

def load_csv(file):
    with open(file, 'r') as f:
        reader = csv.reader(f)
        return dict(reader)
