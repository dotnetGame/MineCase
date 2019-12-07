# CMakeList.txt : CMake project for emerald, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.8)

set(
    EMERALD_CLIENT_LIST
    "emerald.cpp"
)

# Add source to this project's executable.
add_executable (emerald-client ${EMERALD_CLIENT_LIST})

target_compile_definitions(emerald-client PRIVATE VK_USE_PLATFORM_WIN32_KHR)
target_include_directories(emerald-client PRIVATE Vulkan::Vulkan)
target_link_libraries(emerald-client Vulkan::Vulkan)
target_link_libraries(emerald-client ${CONAN_LIBS})

# TODO: Add tests and install targets if needed.
