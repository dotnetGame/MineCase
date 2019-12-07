
include_directories(${PROJECT_SOURCE_DIR}/include/)
include_directories(${PROJECT_SOURCE_DIR}/contrib/spdlog/include)
include_directories(${PROJECT_SOURCE_DIR}/contrib/asio/include)


# Include sub-projects.
add_subdirectory ("minecase-client")
add_subdirectory ("minecase-silo")
add_subdirectory ("minecase-networking")