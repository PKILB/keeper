import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"



class VaultKeepService {
    async createVaultKeep(keepsInVault) {
    const res = await api.post('api/vaultKeeps', keepsInVault )
    AppState.vaultKeep.push(res.data)
    logger.log(keepsInVault)
    }

}

export const vaultKeepsService = new VaultKeepService()