from utils import test_range, get_epsilon

def newton(a, x0):
    epsilon = get_epsilon()
    xn = x0
    i = 0

    while abs(xn - 1 / a) >= epsilon and abs(xn - xn * (2 - xn * a)) >= epsilon:
        i += 1
        xn = xn - xn * (2 - xn * a)
        if abs(xn) > 10 ** 6:
            return 0
        
    return i
            

A = 9
for x0 in test_range(3):
    i = newton(A, x0)
    print(x0, 'inf' if i == 0 else i)
