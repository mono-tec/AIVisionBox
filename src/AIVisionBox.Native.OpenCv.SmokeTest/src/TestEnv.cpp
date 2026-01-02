#include "TestEnv.h"
#include <windows.h>

std::filesystem::path GetExeDir()
{
    wchar_t buf[MAX_PATH]{};
    GetModuleFileNameW(nullptr, buf, MAX_PATH);
    return std::filesystem::path(buf).parent_path();
}

std::filesystem::path GetAssetsDir()
{
    return GetExeDir() / "assets";
}

std::filesystem::path GetOpenCvCardsPngPath()
{
    return GetAssetsDir() / "opencv" / "cards.png";
}
