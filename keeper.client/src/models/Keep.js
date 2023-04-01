import { Profile } from "./Account.js"

export class Keep {
    constructor(data) {
        this.id = data.id
        this.creator = new Profile(data.creator)
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.views = data.views
    }
}

export class KeptKeep extends Keep {
    constructor (data) {
        super(data.keep)
        this.keptKeepId = data.id
    }
}