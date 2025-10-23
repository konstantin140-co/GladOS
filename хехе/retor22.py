import time
import random

def read_array(filename):
    with open(filename, 'r') as f:
        data = f.read().strip()
        return list(map(int, data.split()))

n = 1000
gen_filename = "generation.txt"
sort_filename = "sort.txt"
array_to_sort = read_array(gen_filename)

def bubble_sort(arr):
    comparisons = 0
    swaps = 0
    n = len(arr)
    arr_copy = arr.copy()
    start_time = time.time()

    for i in range(n-1):
        swapped = False
        for j in range(0, n-i-1):
            comparisons += 1
            if arr_copy[j] > arr_copy[j+1]:
                arr_copy[j], arr_copy[j+1] = arr_copy[j+1], arr_copy[j]
                swaps += 1
                swapped = True
        if not swapped:
            break
    end_time = time.time()
    time1 = end_time - start_time
    return arr_copy, comparisons, swaps, time1

def shaker_sort(arr):
    comparisons = 0
    swaps = 0
    arr_copy = arr.copy()
    n = len(arr_copy)
    left = 0
    right = n - 1

    start_time = time.time()

    while left <= right:
        for i in range(left, right):
            comparisons += 1
            if arr_copy[i] > arr_copy[i+1]:
                arr_copy[i], arr_copy[i+1] = arr_copy[i+1], arr_copy[i]
                swaps += 1
        right -= 1

        for i in range(right, left, -1):
            comparisons += 1
            if arr_copy[i-1] > arr_copy[i]:
                arr_copy[i-1], arr_copy[i] = arr_copy[i], arr_copy[i-1]
                swaps += 1
        left += 1

    end_time = time.time()
    time1 = end_time - start_time

    return arr_copy, comparisons, swaps, time1

def merge(left_list, right_list, swaps_count):
    sorted_list = []
    left_list_index = right_list_index = 0

    left_list_length, right_list_length = len(left_list), len(right_list)

    for _ in range(left_list_length + right_list_length):
        if left_list_index < left_list_length and right_list_index < right_list_length:
            if left_list[left_list_index] <= right_list[right_list_index]:
                sorted_list.append(left_list[left_list_index])
                left_list_index += 1
            else:
                sorted_list.append(right_list[right_list_index])
                right_list_index += 1
                swaps_count[0] += left_list_length - left_list_index
        elif left_list_index == left_list_length:
            sorted_list.append(right_list[right_list_index])
            right_list_index += 1
        elif right_list_index == right_list_length:
            sorted_list.append(left_list[left_list_index])
            left_list_index += 1
    return sorted_list

def merge_sort(nums, swaps_count):
    if len(nums) <= 1:
        return nums

    mid = len(nums) // 2

    left_list = merge_sort(nums[:mid], swaps_count)
    right_list = merge_sort(nums[mid:], swaps_count)

    return merge(left_list, right_list, swaps_count)

def use_merge(nums):
    swaps_count = [0]
    start_time = time.time()
    sorted_nums = merge_sort(nums, swaps_count)
    end_time = time.time()
    time1 = end_time - start_time
    return sorted_nums, swaps_count[0], time1

def CombSort(ls):
    arr_copy = ls.copy()
    n = len(arr_copy)
    step = n
    flag = True
    swaps = 0
    start_time = time.time()

    while step > 1 or flag:
        if step > 1:
            step = int(step / 1.25)
        flag = False
        i = 0
        while i + step < n:
            if arr_copy[i] > arr_copy[i + step]:
                arr_copy[i], arr_copy[i + step] = arr_copy[i + step], arr_copy[i]
                swaps += 1
                flag = True
            i += 1
    end_time = time.time()
    time1 = end_time - start_time
    return arr_copy, swaps, time1

def insertion_sort(arr):
    arr_copy = arr.copy()
    swaps = 0
    start_time = time.time()
    for i in range(len(arr_copy)):
        cursor = arr_copy[i]
        pos = i
        while pos > 0 and arr_copy[pos - 1] > cursor:
            arr_copy[pos] = arr_copy[pos - 1]
            swaps += 1
            pos = pos - 1
        arr_copy[pos] = cursor
        if pos != i:
            swaps += 1
    end_time = time.time()
    time1 = end_time - start_time
    return arr_copy, swaps, time1

def selection_sort(alist):
    arr_copy = alist.copy()
    swaps = 0
    start_time = time.time()
    for i in range(0, len(arr_copy) - 1):
        smallest = i
        for j in range(i + 1, len(arr_copy)):
            if arr_copy[j] < arr_copy[smallest]:
                smallest = j
        if smallest != i:
            arr_copy[i], arr_copy[smallest] = arr_copy[smallest], arr_copy[i]
            swaps += 1
    end_time = time.time()
    time1 = end_time - start_time
    return arr_copy, swaps, time1

def ShellSort(data):
    arr_copy = data.copy()
    size = len(arr_copy)
    interval = size // 2
    swaps = 0
    start_time = time.time()
    while interval > 0:
        for i in range(interval, size):
            temp = arr_copy[i]
            j = i
            while j >= interval and arr_copy[j - interval] > temp:
                arr_copy[j] = arr_copy[j - interval]
                swaps += 1
                j -= interval
            arr_copy[j] = temp
            if j != i:
                swaps += 1
        interval //= 2
    end_time = time.time()
    time1 = end_time - start_time
    return arr_copy, swaps, time1

def quicksort(nums, swaps_count):
    if len(nums) <= 1:
        return nums

    q = random.choice(nums)
    s_nums = []
    m_nums = []
    e_nums = []

    for n in nums:
        if n < q:
            s_nums.append(n)
        elif n > q:
            m_nums.append(n)
        else:
            e_nums.append(n)
    if len(s_nums) > 0 or len(m_nums) > 0:
        swaps_count[0] += len(nums) - 1

    return quicksort(s_nums, swaps_count) + e_nums + quicksort(m_nums, swaps_count)

def use_quicksort(nums):
    swaps_count = [0]
    start_time = time.time()
    sorted_nums = quicksort(nums, swaps_count)
    end_time = time.time()
    time1 = end_time - start_time
    return sorted_nums, swaps_count[0], time1

def heapify(nums, heap_size, root_index, swaps_count):
    largest = root_index
    left_child = (2 * root_index) + 1
    right_child = (2 * root_index) + 2

    if left_child < heap_size and nums[left_child] > nums[largest]:
        largest = left_child

    if right_child < heap_size and nums[right_child] > nums[largest]:
        largest = right_child

    if largest != root_index:
        nums[root_index], nums[largest] = nums[largest], nums[root_index]
        swaps_count[0] += 1
        heapify(nums, heap_size, largest, swaps_count)

def heap_sort(nums):
    arr_copy = nums.copy()
    n = len(arr_copy)
    swaps_count = [0]

    for i in range(n, -1, -1):
        heapify(arr_copy, n, i, swaps_count)

    for i in range(n - 1, 0, -1):
        arr_copy[i], arr_copy[0] = arr_copy[0], arr_copy[i]
        swaps_count[0] += 1
        heapify(arr_copy, i, 0, swaps_count)

    return arr_copy, swaps_count[0]

def use_heap(nums):
    start_time = time.time()
    sorted_nums, swaps = heap_sort(nums)
    end_time = time.time()
    time1 = end_time - start_time
    return sorted_nums, swaps, time1

# Основной блок выполнения
array = read_array(gen_filename)

# Вызов всех функций сортировки
bubble_sorted, bubble_comparisons, bubble_swaps, bubble_time = bubble_sort(array)
shaker_sorted, shaker_comparisons, shaker_swaps, shaker_time = shaker_sort(array)
heap_sorted, heap_swaps, heap_time = use_heap(array)
merge_sorted, merge_swaps, merge_time = use_merge(array)
Comb_sorted, Comb_swaps, Comb_time = CombSort(array)
ins_sorted, ins_swaps, ins_time = insertion_sort(array)
sel_sorted, sel_swaps, sel_time = selection_sort(array)
shell_sorted, shell_swaps, shell_time = ShellSort(array)
quic_sorted, quic_swaps, quic_time = use_quicksort(array)

# Вывод результатов в консоль
print("СОРТИРОВКА ПУЗЫРЬКОМ:")
print(f"Время выполнения: {bubble_time:.6f} секунд")
print(f"Количество обменов: {bubble_swaps}")
print("-" * 50)
print("СОРТИРОВКА ШЕЙКЕР:")
print(f"Время выполнения: {shaker_time:.6f} секунд")
print(f"Количество обменов: {shaker_swaps}")
print("-" * 50)
print("СОРТИРОВКА РАСЧЁСКОЙ:")
print(f"Время выполнения: {Comb_time:.6f} секунд")
print(f"Количество обменов: {Comb_swaps}")
print("-" * 50)
print("СОРТИРОВКА ПИРАМИДАЛЬНАЯ:")
print(f"Время выполнения: {heap_time:.6f} секунд")
print(f"Количество обменов: {heap_swaps}")
print("-" * 50)
print("СОРТИРОВКА СЛИЯНИЕМ:")
print(f"Время выполнения: {merge_time:.6f} секунд")
print(f"Количество обменов: {merge_swaps}")
print("-" * 50)
print("СОРТИРОВКА ВСТАВКАМИ:")
print(f"Время выполнения: {ins_time:.6f} секунд")
print(f"Количество обменов: {ins_swaps}")
print("-" * 50)
print("СОРТИРОВКА ВЫБОРОМ:")
print(f"Время выполнения: {sel_time:.6f} секунд")
print(f"Количество обменов: {sel_swaps}")
print("-" * 50)
print("СОРТИРОВКА ШЕЛЛА:")
print(f"Время выполнения: {shell_time:.6f} секунд")
print(f"Количество обменов: {shell_swaps}")
print("-" * 50)
print("БЫСТРАЯ СОРТИРОВКА:")
print(f"Время выполнения: {quic_time:.6f} секунд")
print(f"Количество обменов: {quic_swaps}")
print("-" * 50)
print("СРАВНЕНИЕ АЛГОРИТМОВ:")
print(f"Пузырьковая сортировка: {bubble_time:.6f} сек, обменов: {bubble_swaps}")
print(f"Шейкерная сортировка: {shaker_time:.6f} сек, обменов: {shaker_swaps}")
print(f"Сортировка расчёской: {Comb_time:.6f} сек, обменов: {Comb_swaps}")
print(f"Пирамидальная сортировка: {heap_time:.6f} сек, обменов: {heap_swaps}")
print(f"Сортировка слиянием: {merge_time:.6f} сек, обменов: {merge_swaps}")
print(f"Сортировка вставками: {ins_time:.6f} сек, обменов: {ins_swaps}")
print(f"Сортировка выбором: {sel_time:.6f} сек, обменов: {sel_swaps}")
print(f"Сортировка Шелла: {shell_time:.6f} сек, обменов: {shell_swaps}")
print(f"Быстрая сортировка: {quic_time:.6f} сек, обменов: {quic_swaps}")
s = list(set(bubble_sorted))
# Запись результатов в файл
with open(sort_filename, 'w', encoding='utf-8') as f:
    f.write(' '.join([str(x) for x in s]))

