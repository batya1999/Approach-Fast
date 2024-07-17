from scenario import Scenario
import Config

class Simulator:
    def __init__(self):
        config = Config().configData
        self.scenario = Scenario()
        self.param = config.dx

    def main(self):
        mode = 'Appearance mode'
        while True:
            if mode == 'Appearance mode':
                new_point, mode = self.scenario()
            elif mode == 'Repair mode':
                new_point, mode = self.scenario()
            else:  # Collision mode
                new_point, mode = self.scenario()
            self.draw(new_point)

    def draw(self, point):
        pass





