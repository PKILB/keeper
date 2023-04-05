<template>
    <div class="container-fluid bg-grey pt-2">
        <div class="row">
            <div class="col-5 m-auto">
                <div class="d-flex justify-content-center">
                    <img class="rounded vault-img-height" :src="vault?.img" :alt="vault?.name">
                </div>
                <div class="img-overlay d-flex justify-content-center text-style text-light">
                    <h4>
                        {{ vault?.name }}
                    </h4>
                </div>
                <div class="img-overlay-two d-flex justify-content-center text-style text-light">
                    <p>By {{ vault?.creator?.name }}</p>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12 m-auto d-flex justify-content-center">
                <div class="d-flex">
                    <h6 class="d-flex justify-content-center pe-2">{{ keptKeeps?.length }} Keeps</h6>
                    <select v-model="editable.vaultKeepId" class="pe-2">
                        <option v-for="k in keptKeeps" :value="k.vaultKeepId">{{ k.name }}</option>
                    </select>
                    <button v-if="account.id == vault?.creatorId" class="btn btn-danger text-light rounded ps-2"
                        @click="deleteVaultKeep(vaultKeepId)">
                        Remove
                    </button>
                </div>

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
import { computed, onMounted, watchEffect, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { vaultsService } from '../services/VaultsService.js';
import KeepCard from '../components/KeepCard.vue';
import { vaultKeepsService } from '../services/VaultKeepsService.js';
import { KeptKeep } from '../models/Keep.js';
// import { profile } from 'console';
// import { keepsService } from '../services/KeepsService.js';

export default {
    props: {
        keptKeep: { type: KeptKeep, required: true },

    },
    setup() {
        const editable = ref({})
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
            editable,
            vault: computed(() => AppState.activeVault),
            vaultKeeps: computed(() => AppState.vaultKeep),
            keptKeeps: computed(() => AppState.keptKeeps),
            keptKeep: computed(() => AppState.keptKeep),
            keeps: computed(() => AppState.keeps),
            profile: computed(() => AppState.profile),
            account: computed(() => AppState.account),

            async deleteVaultKeep() {
                // debugger
                try {
                    const vaultKeepId = editable.value

                    if (await Pop.confirm('Are you sure you want to remove this keep from the vault?')) {
                        await vaultKeepsService.deleteVaultKeep(vaultKeepId)
                    }
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            }
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

.text-style {
    text-shadow: 0 0 4px rgb(0, 0, 0);
}

.img-overlay {
    transform: translateY(-10vh);
}

.img-overlay-two {
    transform: translateY(-11vh);
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