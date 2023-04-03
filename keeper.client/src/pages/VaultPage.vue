<template>
    <div class="container-fluid bg-grey">
        <div class="row">
            <div class="col-12">
                <div>
                    <img :src="vault?.img" :alt="vault?.name">
                </div>
            </div>
        </div>

    </div>
</template>


<script>
import { AppState } from '../AppState.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { vaultsService } from '../services/VaultsService.js';

export default {
    setup() {
        const route = useRoute()

        async function getVaultById() {
            try {
                const vaultId = route.params.vaultId
                await vaultsService.getVaultById(vaultId)
            } catch (error) {
                logger.error(error)
                Pop.error(error.message)
            }
        }

        onMounted(() => {
            getVaultById()
        })
        return {
            vault: computed(() => AppState.activeVault)
        }
    }
}
</script>


<style lang="scss" scoped></style>