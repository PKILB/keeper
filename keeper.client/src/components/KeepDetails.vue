<template>
    <div v-if="keep" class="container-fluid rounded">
        <div class="row">
            <div class="col-md-6 p-0 img-height">
                <img class="img-fluid w-100" :src="keep.img" alt="">
            </div>
            <div class="col-md-6">
                <div class="row">
                    <div class="col-12">
                        <div class="row">
                            <div class="d-flex justify-content-center">
                                {{ keep.views }} {{ keep.kept }}
                            </div>
                        </div>
                        <div class="row">
                            <h2 class="d-flex justify-content-center align-items-center">{{ keep.name }}</h2>
                            <p class="d-flex justify-content-center align-content-center px-5">{{ keep.description }}</p>
                        </div>
                        <div class="row">
                            <div class="col justify-content-center">
                                <select v-model="editable.vaultId">
                                    <option disabled value="">Please select one</option>
                                    <option v-for="m in myVaults" :value="m.id"> {{ m.name }} </option>
                                </select>
                                <!-- <select class="form-select">
                                    <option v-for="m in myVaults">{{ m.name }}</option>
                                </select> -->
                            </div>
                            <div class="col">
                                <button class="btn btn-dark text-light text-center rounded"
                                    @click="createVaultKeep(vaultId)">save
                                </button>
                            </div>
                            <div class="col d-flex align-content-center">
                                <img class="profile-img" :src="keep?.creator?.picture" :alt="keep?.creator?.name">
                                <!-- <router-link class="selectable" :to="{ name: 'Profile', params: { profileId: keep?.creatorId } }">
                                </router-link> -->
                                <div class="d-flex align-content-center">
                                    <p class="d-flex fw-bold text-style p-0">{{ keep?.creator?.name }}</p>
                                </div>
                            </div>
                            <!-- <div class="col">
                            </div> -->
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
    max-height: 100%;
    position: relative;
}
</style>