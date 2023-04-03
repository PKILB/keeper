<template>
    <router-link class="selectable" :to="{ name: 'Vault', params: { vaultId: vault.id } }">
        <div class="container my-1">
            <div class="card rounded elevation-1">
                <img class="img-fluid rounded" :src="vault.img" :alt="vault.name">
                <div class="card-img-overlay">
                    <div class="row">
                        <div class="col-12">
                            <div class="col-12" v-if="account.id == vault?.creator?.id">
                                <i @click="deleteVault(vault.id)"
                                    class="d-flex justify-content-end selectable fs-3 text-danger mdi mdi-alpha-x-circle"></i>
                            </div>
                        </div>
                    </div>
                    <div class="row d-flex">
                        <div class="col-12">
                            <h6 class="text-light text-style">{{ vault.name }}</h6>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </router-link>
</template>


<script>
import { Vault } from '../models/Vault.js';
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { vaultsService } from '../services/VaultsService.js';

export default {
    props: { vault: { type: Vault, required: true } },
    setup() {
        return {
            vaults: computed(() => AppState.vaults),
            account: computed(() => AppState.account),

            async deleteVault(vaultId) {
                try {
                    if (await Pop.confirm('Are you sure you want to delete this vault?')) {
                        await vaultsService.deleteVault(vaultId)
                    }
                } catch (error) {
                    logger.error
                    Pop.error(error.message)
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.text-style {
    text-shadow: 0 0 4px rgb(0, 0, 0);
}
</style>