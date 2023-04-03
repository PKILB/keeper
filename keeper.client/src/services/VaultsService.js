import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"



class VaultsService {
    async getProfileVaults(profileId) {
        const res = await api.get('api/profiles/' + profileId + '/vaults')
        logger.log('gotten vaults', res.data)
        AppState.vaults = res.data.map(v => new Vault(v))
    }

    async createVault(formData) {
        const res = await api.post('api/vaults', formData)
        logger.log('creating vault', res.data)
        AppState.vaults.push(res.data)
    }

    async deleteVault(vaultId) {
        const res = await api.delete('api/vaults/' + vaultId)
        logger.log('delete', res.data)
        let i = AppState.vaults.findIndex(v => v.id == vaultId)
        if (i != -1) {
            AppState.vaults.splice(i, 1)
        }
    }
}

export const vaultsService = new VaultsService()