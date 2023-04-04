<template>
    <div class="container-fluid bg-grey py-2">
        <div class="row">
            <div class="col-5 m-auto">
                <div class="d-flex justify-content-center">
                    <img class="rounded vault-img-height" :src="vault?.img" :alt="vault?.name">
                </div>
            </div>
        </div>
        <div class="row pt-2">
            <div class="col-12 m-auto">
                <h6 class="d-flex justify-content-center">{{ keptKeeps?.length }} Keeps</h6>
            </div>
        </div>
        <div class="row">
            <div class="col-md-10 m-auto">
                <section class="bricks">
                    <div v-for="k in keptKeeps" class="pt-2">
                        <KeepCard :keep="k" />
                    </div>
                </section>
            </div>
        </div>
    </div>

    <KeepModalComponent id="keepDetails">
        <KeepDetails />
    </KeepModalComponent>
</template>


<script>
import { AppState } from '../AppState.js';
import { computed, onMounted, watchEffect } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { vaultsService } from '../services/VaultsService.js';
import KeepCard from '../components/KeepCard.vue';
// import { keepsService } from '../services/KeepsService.js';

export default {
    setup() {
        const route = useRoute();
        const router = useRouter();
        async function getVaultById() {
            try {
                const vaultId = route.params.vaultId;
                await vaultsService.getVaultById(vaultId);
            }
            catch (error) {
                logger.error(error);
                Pop.error(error.message);
                router.push({ name: "Home" });
            }
        }
        async function getKeepsInVault() {
            try {
                const vaultId = route.params.vaultId;
                await vaultsService.getKeepsInVault(vaultId);
            }
            catch (error) {
                logger.error(error);
                Pop.error(error.message);
            }
        }
        watchEffect(() => {
            getVaultById();
            getKeepsInVault();
        });
        return {
            vault: computed(() => AppState.activeVault),
            vaultKeeps: computed(() => AppState.vaultKeep),
            keptKeeps: computed(() => AppState.keptKeeps),
            keeps: computed(() => AppState.keeps)
        };
    },
    components: { KeepCard }
}
</script>


<style lang="scss" scoped>
.vault-img-height {
    height: 200px;
    width: 400px;
}

$gap: .5em;

.bricks {
    columns: 300px;
    column-gap: $gap;

    &>div {
        margin-top: $gap;
        display: inline-block;
    }
}
</style>