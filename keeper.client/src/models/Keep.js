import { Profile } from "./Account.js"

export class Keep {
    constructor(data) {
        this.id = data.id
        this.creator = new Profile(data.creator)
        this.creatorId = data.creatorId
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.views = data.views
        this.kept = data.kept
    }
}

export class KeptKeep extends Keep {
    constructor (data) {
        super(data)
        this.vaultKeepId = data.vaultKeepId
    }
}