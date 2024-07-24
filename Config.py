import random
import utills
class Config:
    def __init__(self):
        FileNameList = ["config1", "config2", "config3", "config4", "config5"]
        RanfomConfigFile = random.choice(FileNameList)
        #self.configData = utills.load_csv(RanfomConfigFile)
        self.configData = {
            'p1': (10, 10, 10),
            'p2': (15, 15, 15),
            'dist': (12, 12, 12),
            'pre_dist': 3,
            'mu': 0,
            'sigma': 1,  # std
            'bias': 0,
            'dx': 1 / 10,  # sec
            'case_name': 'test'
        }



