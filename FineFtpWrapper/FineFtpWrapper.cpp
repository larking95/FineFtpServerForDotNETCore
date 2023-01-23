
#include "pch.h"
#include "FineFtpWrapper.h"
#include "fineftp/server.h"

#include <msclr/marshal_cppstd.h>
using namespace msclr::interop;

FineFtpWrapper::FtpServer::FtpServer(System::String^ address, int port)
{
	std::string addressStr = marshal_as<std::string>(address);
	this->mp_nativeInstance = new fineftp::FtpServer(addressStr, port);
}

FineFtpWrapper::FtpServer::FtpServer(int port)
{
	this->mp_nativeInstance = new fineftp::FtpServer(port);
}

FineFtpWrapper::FtpServer::~FtpServer()
{
	this->!FtpServer();
}

FineFtpWrapper::FtpServer::!FtpServer()
{
	delete this->mp_nativeInstance;
}

bool FineFtpWrapper::FtpServer::addUser(System::String^ username, System::String^ password, System::String^ local_root_path, const Permission permissions)
{
	std::string usernameStr				= marshal_as<std::string>(username);
	std::string passwordStr				= marshal_as<std::string>(password);
	std::string local_root_path_str		= marshal_as<std::string>(local_root_path);
	return this->mp_nativeInstance->addUser(usernameStr, passwordStr, local_root_path_str, static_cast<fineftp::Permission>(permissions));
}

bool FineFtpWrapper::FtpServer::addUserAnonymous(System::String^ local_root_path, const Permission permissions)
{
	return false;
}

bool FineFtpWrapper::FtpServer::start(std::size_t thread_count)
{
	return this->mp_nativeInstance->start(thread_count);
}

void FineFtpWrapper::FtpServer::stop()
{
	this->mp_nativeInstance->stop();
}

int FineFtpWrapper::FtpServer::getOpenConnectionCount()
{
	return this->mp_nativeInstance->getOpenConnectionCount();
}

int FineFtpWrapper::FtpServer::getPort()
{
	return this->mp_nativeInstance->getPort();
}

System::String^ FineFtpWrapper::FtpServer::getAddress()
{
	std::string addressStr = this->mp_nativeInstance->getAddress();
	return marshal_as<System::String^>(addressStr);
}
