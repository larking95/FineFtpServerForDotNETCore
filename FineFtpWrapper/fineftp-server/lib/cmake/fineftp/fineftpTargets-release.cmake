#----------------------------------------------------------------
# Generated CMake target import file for configuration "Release".
#----------------------------------------------------------------

# Commands may need to know the format version.
set(CMAKE_IMPORT_FILE_VERSION 1)

# Import target "fineftp::server" for configuration "Release"
set_property(TARGET fineftp::server APPEND PROPERTY IMPORTED_CONFIGURATIONS RELEASE)
set_target_properties(fineftp::server PROPERTIES
  IMPORTED_LINK_INTERFACE_LANGUAGES_RELEASE "CXX"
  IMPORTED_LOCATION_RELEASE "${_IMPORT_PREFIX}/lib/fineftp-server.lib"
  )

list(APPEND _cmake_import_check_targets fineftp::server )
list(APPEND _cmake_import_check_files_for_fineftp::server "${_IMPORT_PREFIX}/lib/fineftp-server.lib" )

# Commands beyond this point should not need to know the version.
set(CMAKE_IMPORT_FILE_VERSION)
