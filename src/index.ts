import "./lib/setup";

import { ApplicationCommandRegistries, LogLevel, SapphireClient } from "@sapphire/framework";
import { GatewayIntentBits } from "discord.js";

const client = new SapphireClient({
	defaultPrefix: "!",
	caseInsensitiveCommands: true,
	logger: {
		level: LogLevel.Debug
	},
	intents: [
		GatewayIntentBits.DirectMessages,
		GatewayIntentBits.GuildMessages,
		GatewayIntentBits.Guilds,
		GatewayIntentBits.MessageContent
	],
	loadMessageCommandListeners: true
});

// Register the guilds where the commands will be registered.
// TODO: make this only in dev mode
ApplicationCommandRegistries.setDefaultGuildIds(["1132282965368520816"]);

const main = async () => {
	try {
		client.logger.info("Logging in");
		await client.login();
		client.logger.info("logged in");
	} catch (error) {
		client.logger.fatal(error);
		await client.destroy();
		process.exit(1);
	}
};

void main();
