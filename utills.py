import csv
import pandas as pd
import matplotlib.pyplot as plt

# def load_csv(file):
#     with open(file, 'r') as f:
#         reader = csv.reader(f)
#         return dict(reader)


def load_csv2(file):
    df = pd.read_csv(file)
    # print(df['pre_dist'])
    return df


def plot_parbulah(a, b, c, x_start, x_end):
    xs = []
    ys = []
    if x_start <= x_end:
        for x in range(x_start, x_end + 1):
            y = a * (x ** 2) + b * x + c
            xs.append(x)
            ys.append(y)
        tuples = list(zip(xs, ys))
        df = pd.DataFrame(tuples, columns=["x", "y"])
        df.plot(kind="line", x="x", y="y", figsize=(7, 7))
        plt.show()
    else:
        for x in range(x_start, x_end + 1, -1):
            y = a * (x ** 2) + b * x + c
            xs.append(x)
            ys.append(y)
        tuples = list(zip(xs, ys))
        df = pd.DataFrame(tuples, columns=["x", "y"])
        df.plot(kind="line", x="x", y="y", figsize=(7, 7))
        plt.show()



