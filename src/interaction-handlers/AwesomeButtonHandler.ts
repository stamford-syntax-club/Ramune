import { InteractionHandler, InteractionHandlerTypes, container } from "@sapphire/framework";
import type { ButtonInteraction } from "discord.js";

export class ButtonHandler extends InteractionHandler {
	public constructor(ctx: InteractionHandler.LoaderContext, options: InteractionHandler.Options) {
		super(ctx, {
			...options,
			interactionHandlerType: InteractionHandlerTypes.Button
		});
	}

	public override parse(interaction: ButtonInteraction) {
		container.logger.info("Button interaction received!", interaction.toJSON());
		if (interaction.customId !== "my-awesome-button") return this.none();

		return this.some();
	}

	public async run(interaction: ButtonInteraction) {
		await interaction.reply({
			content: "Hello from a button interaction handler!",
			// Let's make it so only the person who pressed the button can see this message!
			ephemeral: true
		});
	}
}
