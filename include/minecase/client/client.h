#ifndef MINECASE_CLIENT_CLIENT_H_
#define MINECASE_CLIENT_CLIENT_H_


#include <memory>

#include <spdlog/spdlog.h>

namespace minecase
{
    namespace client
    {
        class Client
        {
		private:
			std::unique_ptr<spdlog::logger> _logger;
        public:
            Client();
            int run();
        };
    }
}

#endif