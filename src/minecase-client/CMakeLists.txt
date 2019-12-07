
set(HEADER_DIR ${PROJECT_SOURCE_DIR}/include/minecase/client)
set(SOURCE_DIR ${PROJECT_SOURCE_DIR}/src/minecase-client)

set(
    MINECASE_CLIENT_LIST
    ${HEADER_DIR}/client.h
    ${SOURCE_DIR}/client.cpp
)

# Add source to this project's executable.
add_executable (minecase-client ${MINECASE_CLIENT_LIST})


# TODO: Add tests and install targets if needed.
