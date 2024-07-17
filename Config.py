import random
import utills
class Config:
    def __init__(self):
        FileNameList = ["config1", "config2", "config3", "config4", "config5"]
        RanfomConfigFile = random.choice(FileNameList)
        self.configData = utills.load_csv(RanfomConfigFile)



