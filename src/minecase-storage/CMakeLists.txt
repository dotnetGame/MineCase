
set(HEADER_DIR ${PROJECT_SOURCE_DIR}/include/minecase/storage)
set(SOURCE_DIR ${PROJECT_SOURCE_DIR}/src/minecase-storage)


set(
    MINECASE_STORAGE_LIST
    ${SOURCE_DIR}/protocol.cpp
)

# Add source to this project's executable.
add_library (minecase-storage ${MINECASE_STORAGE_LIST})


# TODO: Add tests and install targets if needed.
