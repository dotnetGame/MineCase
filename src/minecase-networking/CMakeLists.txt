
set(
    MINECASE_NETWORKING_LIST
    ${PROJECT_SOURCE_DIR}/src/minecase-networking/protocol/emerald.cpp
)

# Add source to this project's executable.
add_library (minecase-networking ${MINECASE_NETWORKING_LIST})


# TODO: Add tests and install targets if needed.
