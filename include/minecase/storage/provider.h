#ifndef MINECASE_STORAGE_PROVIDER_H_
#define MINECASE_STORAGE_PROVIDER_H_


#include <string>
#include <memory>

#include <spdlog/spdlog.h>

namespace minecase
{
    namespace storage
    {
        class StorageProvider
        {
		private:
			std::unique_ptr<spdlog::logger> _logger;
        public:
            StorageProvider();
            virtual void init(std::string name) = 0;

            virtual void close();

            virtual void readState(std::string grainType, GrainReference grainRef, IGrainState grainState);

            virtual void writeState(std::string grainType, GrainReference grainRef, IGrainState grainState);

            virtual void clearState(std::string grainType, GrainReference grainRef, IGrainState grainState);
        };
    }
}

#endif