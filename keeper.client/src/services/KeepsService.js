import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"



class KeepsService {
    async getAllKeeps() {
        const res = await api.get('api/keeps')
        logger.log('getting all keeps', res.data)
        const keeps = res.data.map(k => new Keep(k))
        AppState.keeps = keeps
    }

    async getProfileKeeps(profileId) {
        const res = await api.get('api/profiles/' + profileId + '/keeps')
        logger.log(res.data, 'gotten profile keeps')
        AppState.keeps = res.data.map(k => new Keep(k))
    }

    async getKeepById(keepId)
    {
        const res = await api.get('api/keeps/' + keepId)
        AppState.keep = new Keep(res.data)
    }

    // async getKeepById(keepId) {
    //     const res = await api.get('api/keeps/' + keepId)
    //     AppState.keep = new Keep(res.data)
    // AppState.keeps.find(k => k.id == keepId)
    // }

    async createKeep(formData) {
        const res = await api.post('api/keeps', formData)
        logger.log('creating keep', res.data)
        AppState.keeps.push(res.data)
    }

    async deleteKeep(keepId) {
        const res = await api.delete('api/keeps/' + keepId)
        logger.log('delete', res.data)
        let i = AppState.keeps.findIndex(k => k.id == keepId)
        if (i != -1) {
            AppState.keeps.splice(i, 1)
        }
    }

    async updateKeep(keepId) {
        const res = await api.put('api/keeps/' + keepId)
        AppState.keeps = new Keep(res.data)
    }
}

export const keepsService = new KeepsService()