import matplotlib.pyplot as plt
import numpy as np

from scenario import Scenario
from Config import Config


class Simulator:
    def __init__(self):
        config = Config()
        self.scenario = Scenario(config)
        self.param = config.dx
        #self.points = self.scenario.dronePath

        # Setup the plot
        self.fig, self.ax = plt.subplots()
        self.ax.set_xlim(-10, 10)
        self.ax.set_ylim(-10, 10)
        self.scatter = self.ax.scatter([], [])

    def main(self):
        for i in range(len(self.scenario.dronePath)):
            new_point = self.scenario.update(self.scenario.dronePath[i])
            self.scenario.dronePath[i] = new_point

        self.draw()

    def draw(self):
        self.scatter.set_offsets(self.scenario.dronePath)
        self.scatter.set_offsets(self.scenario.dronePath)
        plt.draw()
        plt.show()
        print()




if __name__ == "__main__":
    simulator = Simulator()
    plt.ion()  # Turn on interactive mode for live plotting
    simulator.main()
