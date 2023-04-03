<template>
    <div class="container-fluid bg-grey">
        <div class="row mt-3">
            <div class="col-12"></div>
            <div class="col-12 d-flex justify-content-center">
                <img class="profile-img rounded-circle" :src="profile.picture" :alt="profile.name">
            </div>
            <div class="col-12 d-flex justify-content-center">
                <h2>
                    {{ profile.name }}
                </h2>
            </div>
        </div>
        <div class="row">
            <div class="d-flex justify-content-center">
                <p>
                    # of Vaults | # of Keeps
                </p>
            </div>
        </div>
        <div class="row mb-2">
            <div class="col-9 m-auto">
                <div class="row">
                    <h2>Vaults</h2>
                    <div v-for="v in vaults" class="col-3">
                        <VaultCard class="" :vault="v" />
                    </div>
                </div>
            </div>
            <!-- <div class="row d-flex">
                </div> -->
        </div>
        <div class="row">
            <div class="col-9 m-auto">
                <div class="row">
                    <h2>Keeps</h2>
                    <section class="bricks">
                        <div v-for="k in keeps" class="">
                            <KeepCard :keep="k" />
                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>

    <KeepModalComponent id="keepDetails">
        <KeepDetails />
    </KeepModalComponent>
</template>


<script>
import { onMounted, computed, watchEffect } from 'vue';
import { AppState } from '../AppState.js';
import { useRoute, useRouter } from 'vue-router';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { profilesService } from '../services/ProfilesService.js'
import { vaultsService } from '../services/VaultsService.js'
import VaultCard from '../components/VaultCard.vue';
import { keepsService } from '../services/KeepsService.js';
import KeepCard from '../components/KeepCard.vue';

export default {
    setup() {
        const route = useRoute();
        watchEffect(() => {
            getProfileById();
            getProfileVaults();
            getProfileKeeps();
        });
        async function getProfileById() {
            try {
                // const profileId = route.params.profileId
                // logger.log(profileId)
                await profilesService.getProfileById(route.params.profileId);
            }
            catch (error) {
                logger.error(error);
                Pop.error(error.message);
            }
        }
        async function getProfileVaults() {
            try {
                const profileId = route.params.profileId;
                await vaultsService.getProfileVaults(profileId);
            }
            catch (error) {
                logger.log(error);
                Pop.error(error.message);
            }
        }

        async function getProfileKeeps() {
            try {
                const profileId = route.params.profileId
                await keepsService.getProfileKeeps(profileId)
            } catch (error) {
                logger.log(error)
                Pop.error(error.message)
            }
        }
        return {
            profile: computed(() => AppState.profile),
            vaults: computed(() => AppState.vaults),
            keeps: computed(() => AppState.keeps)
        };
    },
    components: { VaultCard, KeepCard }
}
</script>


<style lang="scss" scoped>
.profile-img {
    border-radius: 50%;
    height: 90px;
    width: 107px;
    // border: 2px solid black;
    // box-shadow: 0px 2px 0px black;
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