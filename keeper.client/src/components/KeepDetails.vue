<template>
    <div v-if="keep" class="container-fluid img-height rounded">
        <div class="row">
            <div class="col-md-6 p-0">
                <img class="img-height w-100" :src="keep.img" alt="">
            </div>
            <div class="col-md-6 d-flex flex-column justify-content-between">
                <div class="row pt-4">
                    <div class="d-flex justify-content-center">
                        <i class="mdi mdi-eye pe-1"></i>
                        {{ keep.views }}
                        <div class="ps-4">
                            K {{ keep.kept }}
                        </div>
                    </div>
                </div>
                <div class="row">
                    <h2 class="d-flex align-content-center justify-content-center">{{ keep.name }}</h2>
                    <p class="d-flex justify-content-center align-content-center px-5">{{ keep.description }}</p>
                </div>
                <div class="row">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-4 d-flex justify-content-center">
                                <select v-model="editable.vaultId">
                                    <option disabled value="Please Select One">Please select one</option>
                                    <option v-for="m in myVaults" :value="m.id"> {{ m.name }} </option>
                                </select>

                            </div>
                            <div class="col-4">
                                <button v-if="account.id" class="btn btn-dark text-light text-center rounded"
                                    @click="createVaultKeep(vaultId)">save
                                </button>
                                <div v-else>Go Log In</div>
                            </div>
                            <div class="col-4 d-flex justify-content-center">
                                <div class="d-flex">
                                    <img class="profile-img" :src="keep?.creator?.picture" :alt="keep?.creator?.name">

                                    <p class="d-flex fw-bold text-style p-0">{{ keep?.creator?.name
                                    }}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed, ref } from 'vue';
import { AppState } from '../AppState.js';
import { Keep } from '../models/Keep.js';
import { vaultsService } from '../services/VaultsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { Vault } from '../models/Vault.js';
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { router } from '../router.js';
export default {
    props: {
        keep: { type: Keep, required: true },
        vault: { type: Vault, required: true }
    },

    setup(props) {
        const editable = ref({ ...props.vault })
        return {
            editable,

            keep: computed(() => AppState.keep),
            vault: computed(() => AppState.activeVault),
            myVaults: computed(() => AppState.myVaults),
            vaultKeep: computed(() => AppState.vaultKeep),
            keptKeep: computed(() => AppState.keptKeeps),
            account: computed(() => AppState.account),
            async getMyVaults() {
                try {
                    let profileId = AppState.account.id
                    await vaultsService.getMyVaults(profileId)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            },

            async createVaultKeep() {
                try {
                    // const formData = editable.value
                    // const keep = this.keptKeep
                    const keepId = AppState.keep.id
                    const vaultId = editable.value
                    logger.log(keepId)
                    logger.log(vaultId)
                    const keepsInVault = {
                        keepId,
                        ...vaultId
                    }
                    // const keptKeep = this.keep.id + formData
                    await vaultKeepsService.createVaultKeep(keepsInVault)
                    if (keepsInVault.keptKeepId) {
                        router.push({ name: 'Vault', params: { vaultId: vault.id } })
                    }
                    this.keep.kept++
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            }
            // profile: computed(() => AppState.profile)
        }
    }
}
</script>


<style lang="scss" scoped>
.profile-img {
    border-radius: 50%;
    height: 7vh;
    width: 7vh;
}

.text-style {
    font-size: 13px;
}

.btn-size {
    height: 30px;
    width: 50px;
}

.img-height {
    max-height: 600px;
    // position: relative;
}
</style>