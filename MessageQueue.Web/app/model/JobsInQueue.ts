module MQPractice.Model {
	
	export class JobsInQueue {
		public createdJobIds: string[];

		public completeJobIds: string[];

		public processingJobIds: string[];

		public errorJobIds: string[];

		public successJobIds: string[];

		public message:string;

		constructor() {
			this.createdJobIds = [];
			this.completeJobIds = [];
			this.processingJobIds = [];
			this.errorJobIds = [];
			this.successJobIds = [];
		}

	}

} 