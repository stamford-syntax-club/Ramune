import { InteractionHandler, InteractionHandlerTypes } from "@sapphire/framework";
import type { ButtonInteraction } from "discord.js";

export class ButtonHandler extends InteractionHandler {
	public constructor(ctx: InteractionHandler.LoaderContext, options: InteractionHandler.Options) {
		super(ctx, {
			...options,
			interactionHandlerType: InteractionHandlerTypes.Button
		});
	}

	public override parse(interaction: ButtonInteraction) {
		return interaction.customId === "my-awesome-button" ? this.some() : this.none();
	}

	public async run(interaction: ButtonInteraction) {
		await interaction.reply({
			content: "Hello from a button interaction handler!",
			// Let's make it so only the person who pressed the button can see this message!
			ephemeral: true
		});
	}
}
