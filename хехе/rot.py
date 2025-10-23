import math
import time
from bisect import bisect_left

def read_array(filename):
    with open(filename, 'r') as f:
        data = f.read().strip()
        return list(map(int, data.split()))
arr = read_array("sort.txt")
#arr = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32]
target = int(input(f"Есть массив:{arr}\n"
                   "Какое число надо найти: "))

def LinearSearch(lys, element):
    start_time = time.time()
    for i in range (len(lys)):
        if lys[i] == element:
            end_time = time.time()
            time1 = end_time - start_time
            return i,time1
    end_time = time.time()
    time1 = end_time - start_time
    return -1,time1

def binary_search_recursive(arr, target, left, right):
    start_time = time.time()
    if left > right:
        return -1,0 # Базовый случай: элемент не найден
    mid = (left + right) // 2 # Находим середину массива
    if arr[mid] == target:
        end_time = time.time()
        time1 = end_time - start_time
        return mid,time1 # Базовый случай: элемент найден
    elif arr[mid] < target:
        return binary_search_recursive(arr, target, mid + 1, right) #Ищем в правой половине
    else:
        return binary_search_recursive(arr, target, left, mid - 1) #Ищем в левой половине


# # Пример использования:

def JumpSearch(lys, val):
    start_time = time.time()
    length = len(lys)
    if length == 0:
        end_time = time.time()
        time1 = end_time - start_time
        return -1, time1
    jump = int(math.sqrt(length))
    left = 0
    right = 0
    while right < length and lys[right] < val:
        left = right
        right = min(length - 1, right + jump)
    if right >= length and lys[length - 1] < val:
        end_time = time.time()
        time1 = end_time - start_time
        return -1, time1
    for i in range(left, min(right + 1, length)):
        if lys[i] == val:
            end_time = time.time()
            time1 = end_time - start_time
            return i, time1
    end_time = time.time()
    time1 = end_time - start_time
    return -1, time1


#____________________________________________________________________________#
def FibonacciSearch(lys, val):
    start_time = time.time()
    if len(lys) == 0:
        end_time = time.time()
        time1 = end_time - start_time
        return -1, time1
    fibM_minus_2 = 0
    fibM_minus_1 = 1
    fibM = fibM_minus_1 + fibM_minus_2
    while fibM < len(lys):
        fibM_minus_2 = fibM_minus_1
        fibM_minus_1 = fibM
        fibM = fibM_minus_1 + fibM_minus_2
    index = -1
    while fibM > 1:
        i = min(index + fibM_minus_2, len(lys) - 1)
        if lys[i] < val:
            fibM = fibM_minus_1
            fibM_minus_1 = fibM_minus_2
            fibM_minus_2 = fibM - fibM_minus_1
            index = i
        elif lys[i] > val:
            fibM = fibM_minus_2
            fibM_minus_1 = fibM_minus_1 - fibM_minus_2
            fibM_minus_2 = fibM - fibM_minus_1
        else:
            end_time = time.time()
            time1 = end_time - start_time
            return i, time1
    if fibM_minus_1 and index + 1 < len(lys) and lys[index + 1] == val:
        end_time = time.time()
        time1 = end_time - start_time
        return index + 1, time1
    end_time = time.time()
    time1 = end_time - start_time
    return -1, time1


#____________________________________________________________________________#
def ExponentialSearch(lys, val):
    start_time = time.time()
    if lys[0] == val:
        return 0
    index = 1
    while index < len(lys) and lys[index] <= val:
        index = index * 2
    res = bisect_left(lys[:min(index, len(lys))],val)
    end_time = time.time()
    time1 = end_time - start_time
    return res,time1

def InterpolationSearch(lys, val):
    start_time = time.time()
    low = 0
    high = (len(lys) - 1)
    while low <= high and val >= lys[low] and val <= lys[high]:
        index = low + int(((float(high - low) / ( lys[high] - lys[low])) * ( val - lys[low])))
        if lys[index] == val:
            end_time = time.time()
            time1 = end_time - start_time
            return index,time1
        if lys[index] < val:
            low = index + 1
        else:
            high = index - 1
    return -1,0

#print("0")
Linear_search_res,Linear_search_time = LinearSearch(arr,target)
#print("1")
binary_search_recursive_result,binary_search_recursive_time = binary_search_recursive(arr, target, 0, len(arr) - 1)
#print("2")
JumpSearch_result, JumpSearch_time = JumpSearch(arr,target)
#print("3")
FibonacciSearch_result,FibonacciSearch_time = FibonacciSearch(arr,target)
#print("4")
ExponentialSearch_result,ExponentialSearch_time = ExponentialSearch(arr,target)
#print("5")
InterpolationSearch_result,InterpolationSearch_time = InterpolationSearch(arr, target)
#print("6")


print(f"\t///линейный:\n Обьект {target} найден на позиции {Linear_search_res} \n Время выполнения: {Linear_search_time:.6f} секунд")
print(f"\t///бинарный рекурсивный:\n Обьект {target} найден на позиции {binary_search_recursive_result} \n Время выполнения: {binary_search_recursive_time:.6f} секунд")
print(f"\t///бинарный прыжковый:\n Обьект {target} найден на позиции {JumpSearch_result} \n Время выполнения: {JumpSearch_time:.6f} секунд")
print(f"\t///фибоначи:\n Обьект {target} найден на позиции {FibonacciSearch_result} \n Время выполнения: {FibonacciSearch_time:.6f} секунд")
print(f"\t///экспонициальный:\n Обьект {target} найден на позиции {ExponentialSearch_result} \n Время выполнения: {ExponentialSearch_time:.6f} секунд")
print(f"\t///итерполяционный:\n Обьект {target} найден на позиции {InterpolationSearch_result} \n Время выполнения: {InterpolationSearch_time:.6f} секунд")




