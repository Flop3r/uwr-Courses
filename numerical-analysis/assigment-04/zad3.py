def bisection(a, b, fun, iterations):
    midpoint = (a + b) / 2
    if fun(midpoint) == 0 or iterations == 0:
        return midpoint
    if fun(a) * fun(midpoint) < 0:
        return bisection(a, midpoint, fun, iterations - 1)
    
    return bisection(midpoint, b, fun, iterations - 1)

F = lambda x: x - 0.49
a_0 = 0
b_0 = 1

target_value = 0.49

for i in range(1, 6):
    theoretical_error = abs((2 ** (-1 - i)) * (b_0 - a_0))
    approx_value = bisection(a_0, b_0, F, i)
    absolute_error = abs(target_value - approx_value)

    print(i, theoretical_error, absolute_error, approx_value)
