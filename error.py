import numpy as np
import config

class Errors():
    def __init__(self, config):
        self.mu = config.mu
        self.sigma = config.sigma
        self.bias = config.bias

    def get_value_with_error(self, val):
        value = np.random.normal(self.mu+self.bias, self.sigma)
        return val + value*val


def main():
    errors_instance = Errors(config)
    for i in range(10):
        print(errors_instance.get_value_with_error(val=1))


if __name__ == '__main__':
    main()
