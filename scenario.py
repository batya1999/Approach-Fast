import Config
import error
import case
class Scenario:
    def __init__(self):
        config = Config.Config().configData
        self.p1 = config.p1
        self.p2 = config.p2
        self.dis = config.dis
        self.error = error.Errors(config)
        self.case = case.Case(config.name_case, self.p1,  self.p2, self.dis)
        self.dronePath = self.case.dronePath
        self.realDronePoint = (0, 0, 0)

    def update(self, oldPoint, newPoint, PredictedPoint, desiredPoint):
        "find the new point with error and pid on xy and th"
        self.PIDalt = self.Pid(self.scenario.p1[2])# z
        self.PIDxy = self.Pid((self.scenario.p1[0], self.scenario.p1[1]))  # x, y
        #self.DronPatch =

    def Pid(self, x):
        pass
