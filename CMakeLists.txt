# CMakeList.txt : Top-level CMake project file, do global configuration
# and include sub-projects here.
#
cmake_minimum_required (VERSION 3.8)

project ("minecase")

set(CMAKE_CXX_STANDARD 17)


# Include sub-projects.
add_subdirectory ("contrib")
add_subdirectory ("src")
