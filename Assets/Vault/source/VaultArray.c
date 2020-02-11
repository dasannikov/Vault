// Shared library build
// WIN: gcc -O3 -std=c11 -Wall -shared -o Vault.dll VaultArray.c
// OSX: clang -O3 -std=c11 -Wall -dynamiclib -o Vault.dylib VaultArray.c

#include <stdint.h>
#include <stdlib.h>
#include <string.h>

// #define DLLEXPORT __declspec(dllexport)
#define DLLEXPORT

// Vault info struct
typedef struct {
    int32_t count;
    int32_t count_max;
    int32_t size_of_element;
} vault_array_info;

// Vault internal function. Return nearest next pov of x.
int32_t _vault_next_pow(int32_t x) {
    if (!x)
        return x;
    int32_t y = 1;
    while (y < x)
        y += y;
    return y;
}

// Init vault. num_of_elements can be zero
DLLEXPORT void* vault_array_init(int32_t size_of_element, int32_t num_of_elements) {
    int32_t count_max = _vault_next_pow(num_of_elements);
    char* full_data = (char*)malloc(size_of_element * count_max + sizeof(vault_array_info));

    vault_array_info* vault_pointr = (vault_array_info*)full_data;
    vault_pointr->count = num_of_elements;
    vault_pointr->count_max = count_max;
    vault_pointr->size_of_element = size_of_element;

    void* data = full_data + sizeof(vault_array_info);
    return data;
}

// Init vault and set default values. num_of_elements can be zero
DLLEXPORT void* vault_array_init_with(int32_t size_of_element, int32_t num_of_elements, void* init_element) {
    char* vault = (char*)vault_array_init(size_of_element, num_of_elements);
    for (int32_t i = 0; i != num_of_elements; i++) {
        char* element_data = vault + size_of_element * i;
        memcpy(element_data, init_element, size_of_element);
    }
    return vault;
}

// Get vault info struct
DLLEXPORT vault_array_info* vault_array_get_info(void* vault) {
    return (vault_array_info*)(((char*)vault) - sizeof(vault_array_info));
}

// Delete vault. Free memory
DLLEXPORT void vault_array_delete(void* vault) {
    free(vault_array_get_info(vault));
}

// Get vault elements count
DLLEXPORT int32_t vault_array_get_count(void* vault) {
    vault_array_info* info = vault_array_get_info(vault);
    return info->count;
}

// Get vault elements count max
DLLEXPORT int32_t vault_array_get_count_max(void* vault) {
    vault_array_info* info = vault_array_get_info(vault);
    return info->count_max;
}

// Resize vault
DLLEXPORT void* vault_array_resize(void* vault, int32_t new_count) {
    vault_array_info* info = vault_array_get_info(vault);

    if (new_count <= info->count_max) {
        info->count = new_count;
        return vault;
    }

    int32_t new_count_max = _vault_next_pow(new_count);
    int32_t new_size = info->size_of_element * new_count_max + sizeof(vault_array_info);
    vault_array_info* full_data = (vault_array_info*)realloc(info, new_size);

    full_data->count = new_count;
    full_data->count_max = new_count_max;

    char* new_vault = (char*)full_data + sizeof(vault_array_info);
    return new_vault;
}

// Add element to the end of vault
DLLEXPORT void* vault_array_add(void* vault, void* element) {
    int32_t old_count = vault_array_get_count(vault);
    vault = vault_array_resize(vault, old_count + 1);
    vault_array_info* info = vault_array_get_info(vault);

    void* element_position = ((char*)vault) + info->size_of_element * old_count;
    memcpy(element_position, element, info->size_of_element);

    return vault;
}

// [SLOW] Remove element at [index] position from vault. Move tail to front.
DLLEXPORT void vault_array_remove(void* vault, int32_t index) {
    vault_array_info* info = vault_array_get_info(vault);
    int32_t new_count = info->count - 1;
    if (index > new_count)
        return;
    info->count = new_count;
    if (index < new_count) {
        char* dest = ((char*)vault) + info->size_of_element * index;
        char* src = dest + info->size_of_element;
        int32_t size = (new_count - index) * info->size_of_element;
        memmove(dest, src, size);
    }
}

// [FAST] Remove element at [index] position from vault. Swap last to [index].
DLLEXPORT void vault_array_remove_swap(void* vault, int32_t index) {
    vault_array_info* info = vault_array_get_info(vault);
    int32_t new_count = info->count - 1;
    if (index > new_count)
        return;
    info->count = new_count;
    if (index < new_count) {
        char* dest = ((char*)vault) + info->size_of_element * index;
        char* src = ((char*)vault) + info->size_of_element * new_count;
        memcpy(dest, src, info->size_of_element);
    }
}

// [FAST] Remove element at [index] position from vault. Swap last to [index].
DLLEXPORT void vault_array_swap(void* vault, int32_t index1, int32_t index2) {
    vault_array_info* info = vault_array_get_info(vault);
    char* dest = ((char*)vault) + info->size_of_element * index1;
    char* src = ((char*)vault) + info->size_of_element * index2;
    memcpy(dest, src, info->size_of_element);
}
