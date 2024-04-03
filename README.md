# Ramune + Sapphire

This is the bot that sends a message to the channel when a review is submitted on the [CourseCompose](https://github.com/stamford-syntax-club/course-compose) website.
It will read a Kafka topic and send a message to the channel when a review is submitted.

The bot is using the [sapphire framework][sapphire], and written in TypeScript.

## How to use?

### Prerequisite

```sh
bun install
```

### Development

This example can be run with `bun run dev` or `bun run watch:start` to watch the files and automatically restart your bot.
We recommend using `bun run dev` as the bot constantly restarting and trying to connect to the gateway can cause rate limits.

### Production
```sh
bun run build # This will build the bot and put the output in the dist folder
```

After building the bot, you can run the bot using `node dist/index.js`.

Keep in mind that we're not using the bun runtime to run the bot in production due to its limitations. It is still not exactly production ready, and in the dev environments, especially on Windows, there are still issues.

## License

Dedicated to the public domain via the [Unlicense], courtesy of the Sapphire Community and its contributors.

[sapphire]: https://github.com/sapphiredev/framework
[unlicense]: https://github.com/sapphiredev/examples/blob/main/LICENSE.md
