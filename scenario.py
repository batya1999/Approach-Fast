import Config
import error
import case
class Scenario:
    def __init__(self, config):
        self.config = config
        self.error = error.Errors(config)

        # case = Case('Test Case', (0, 0, 0), (10, 10, 10), (15, 15, 15), 5)


        self.case = case.Case(self.config.missle_type, (0, 0, 0), self.config.hit_point, self.config.intercept_point, self.config.pre_dist)
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
        # return pid_x, pid_y #todo: add z
        return old_point



    def Pid(self, x):
        #todo: add pid
        return x

#
# def main():
#     s = Scenario()
#
#
# if __name__ == '__main__':
#     main()