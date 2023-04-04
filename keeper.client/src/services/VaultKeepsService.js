import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"



class VaultKeepService {
    async createVaultKeep(keptKeep) {
    const res = await api.post('api/vaultKeeps', keptKeep )
    AppState.vaultKeep.push(res.data)
    logger.log(keptKeep)
    }

}

export const vaultKeepsService = new VaultKeepService()