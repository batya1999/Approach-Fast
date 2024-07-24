import Config
import error
import case
class Scenario:
    def __init__(self):
        config = Config.Config().configData
        self.p1 = config['p1']
        self.p2 = config['p2']
        self.dis = config['dist']
        self.error = error.Errors(config)
        self.realDronePoint = (0, 0, 0)
        self.pre_dist = config['pre_dist']
        # case = Case('Test Case', (0, 0, 0), (10, 10, 10), (15, 15, 15), 5)
        self.case = case.Case(config['case_name'], self.realDronePoint, self.p1, self.p2, self.pre_dist)
        self.dronePath = self.case.dronePath_array


    def update(self, old_point):
        """
        find the new point with error and pid on xy and th
        """
        point_with_error = (self.error.get_value_with_error(old_point[0]),
                            self.error.get_value_with_error(old_point[1]))
                            #self.error.get_value_with_error(old_point[2]))
        #pid_z = self.Pid(point_with_error[2])
        pid_x = self.Pid(point_with_error[0])
        pid_y = self.Pid(point_with_error[1])
        return pid_x, pid_y #todo: add z



    def Pid(self, x):
        #todo: add pid
        return x
