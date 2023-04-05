import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"



class VaultKeepService {
    async createVaultKeep(keepsInVault) {
    const res = await api.post('api/vaultKeeps', keepsInVault )
    AppState.vaultKeep.push(res.data)
    logger.log(keepsInVault)
    }

    async deleteVaultKeep(vaultKeepId) {
        logger.log(vaultKeepId)
        const res = await api.delete('api/vaultkeeps/' + vaultKeepId )
        let i = AppState.keptKeeps.findIndex(k => k.id == vaultKeepId)
        if (i != -1) {
            AppState.keptKeeps.splice(i, 1)
            
        }
    }

}

export const vaultKeepsService = new VaultKeepService()