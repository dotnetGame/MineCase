
set(HEADER_DIR ${PROJECT_SOURCE_DIR}/include/minecase/networking)
set(SOURCE_DIR ${PROJECT_SOURCE_DIR}/src/minecase-networking)


set(
    MINECASE_NETWORKING_LIST
    ${SOURCE_DIR}/protocol/protocol.cpp
)

# Add source to this project's executable.
add_library (minecase-networking ${MINECASE_NETWORKING_LIST})


# TODO: Add tests and install targets if needed.
