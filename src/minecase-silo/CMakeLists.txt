
set(HEADER_DIR ${PROJECT_SOURCE_DIR}/include/minecase/silo)
set(SOURCE_DIR ${PROJECT_SOURCE_DIR}/src/minecase-silo)

set(
    MINECASE_SILO_LIST
    ${SOURCE_DIR}/silo.cpp
)

# Add source to this project's executable.
add_executable (minecase-silo ${MINECASE_SILO_LIST})


# TODO: Add tests and install targets if needed.
