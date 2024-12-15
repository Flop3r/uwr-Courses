from utils import test_range
from math import sqrt

def newton(initial_guess, value, iterations=5):
    current_guess = initial_guess
    for _ in range(iterations):
        current_guess *= 0.5 * (3 - value * current_guess ** 2)

    return current_guess

A = 9
EXPECTED_VALUE = 1 / sqrt(A)

for initial_guess in test_range(5):
    calculated_result = newton(initial_guess, A)
    relative_error = abs(calculated_result - EXPECTED_VALUE) / EXPECTED_VALUE * 100
    print(initial_guess, relative_error)