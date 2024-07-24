import numpy as np
from shapely.geometry import Polygon


class Case:
    def __init__(self, name, p1, p2, dist, pre_dist):
        self.name = name
        self.path_array, self.path_poly = self.get_parabola(p1, p2, dist, pre_dist)
        self.dronePath_array, self.dronePath_poly = self.get_parabola((0, 0, 0), p2, dist, pre_dist)

    def get_parabola(self, drone_pos, p2, dist, pre_dist):
        # Calculate vx and vy using the correct points
        vx = dist[0] - pre_dist - p2[0]
        vy = dist[1] - pre_dist - p2[1]

        # Creating matrix A and vector b for solving the parabola coefficients
        A = np.array([
            [drone_pos[0] ** 2, drone_pos[0], 1],
            [dist[0] ** 2, dist[0], 1],
            [2 * dist[0], 1, 0]
        ])
        b = np.array([drone_pos[1], dist[1], vy / vx])

        # Solve for coefficients a, b, c
        coefficients = np.linalg.solve(A, b)
        a, b, c = coefficients

        # Print coefficients for debugging
        print(f"Coefficients - a: {a}, b: {b}, c: {c}")

        # Create and return the polygon
        poly = Polygon([(drone_pos[0], drone_pos[1]), (dist[0], dist[1]), (p2[0], p2[1])])
        # Calculate 100 points along the polygon's boundary
        boundary = poly.boundary
        length = boundary.length
        points = [boundary.interpolate(i / 100, normalized=True) for i in range(100)]

        # Convert to an array of tuples
        points_array = [(point.x, point.y) for point in points]
        return points_array, poly


# Example usage
#case = Case('Test Case', (0, 0, 0), (10, 10, 10), (15, 15, 15), 5)
