
#include <memory>

#include <minecase/client/client.h>

#include <spdlog/spdlog.h>
#include <spdlog/sinks/basic_file_sink.h>
#include <spdlog/sinks/stdout_color_sinks.h>

int main()
{
	minecase::client::Client client_app;
	client_app.run();

	return 0;
}

namespace minecase
{
	namespace client
	{
		Client::Client()
			: _logger(std::unique_ptr<spdlog::logger>(nullptr))
		{
			auto console_sink = std::make_shared<spdlog::sinks::stdout_color_sink_mt>();
			console_sink->set_level(spdlog::level::warn);
			console_sink->set_pattern("[multi_sink_example] [%^%l%$] %v");

			auto file_sink = std::make_shared<spdlog::sinks::basic_file_sink_mt>("logs.txt", true);
			file_sink->set_level(spdlog::level::trace);

			_logger = std::unique_ptr<spdlog::logger>(new spdlog::logger("client_logger", { console_sink, file_sink }));
			_logger->set_level(spdlog::level::debug);
			_logger->info("client logger start");
		}

		int Client::run()
		{
			return 0;
		}
	}
}