import { AppState } from "../AppState.js"
import { Profile } from "../models/Account.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"



class ProfilesService {
    async getProfileById(profileId) {
        const res = await api.get('api/profiles/' + profileId)
        logger.log('getting profile', res.data)
        AppState.profile = new Profile(res.data)
    }

}

export const profilesService = new ProfilesService()