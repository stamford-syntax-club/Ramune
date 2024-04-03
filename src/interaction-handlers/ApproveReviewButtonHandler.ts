import { InteractionHandler, InteractionHandlerTypes } from "@sapphire/framework";
import { userMention, type ButtonInteraction } from "discord.js";

export class ApproveReviewButtonHandler extends InteractionHandler {
	public constructor(ctx: InteractionHandler.LoaderContext, options: InteractionHandler.Options) {
		super(ctx, {
			...options,
			interactionHandlerType: InteractionHandlerTypes.Button
		});
	}

	public override parse(interaction: ButtonInteraction) {
		return interaction.customId === "approve_review_button" ? this.some() : this.none();
	}

	public async run(interaction: ButtonInteraction) {
		const originalMessage = interaction.message;

		const member = interaction.member;
		let memberMention = member ? userMention(member?.user.id) : "UNKNOWN";

		await originalMessage.delete();
		await interaction.reply({
			content: `Review approved by ${memberMention}.`
		});
	}
}
