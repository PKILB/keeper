<template>
    <div class="modal-content">
        <div class="modal-header">
            <h1 class="modal-title fs-5" id="createVaultModal">Add Your Vault</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <form @submit.prevent="createVault()">
            <div class="modal-body">
                <div class="mb-3">
                    <label for="name" class="form-label">Name...</label>
                    <input v-model="editable.name" required type="text" class="form-control" id="name"
                        aria-describedby="emailHelp">
                </div>
                <div class="mb-3">
                    <label for="img" class="form-label">Image URL...</label>
                    <input v-model="editable.img" required type="url" class="form-control" id="img"
                        aria-describedby="emailHelp">
                </div>
                <div class="mb-3">
                    <label for="description" class="form-label">Vault Description</label>
                    <input v-model="editable.description" required type="textarea" class="form-control" id="description"
                        aria-describedby="emailHelp">
                </div>
            </div>
            <div class="modal-footer">
                <div>
                    <p>Private vaults can only be viewed by you</p>
                    <div class="form-check">
                        <input v-model="editable.isPrivate" class="form-check-label" type="checkbox" value=""
                            id="isPrivate">
                        <label class="form-check-label" for="flexCheckDefault">
                            Make Vault Private
                        </label>
                    </div>
                </div>
                <button type="submit" class="btn btn-primary" data-bs-dismiss="modal">
                    Create
                </button>
            </div>
        </form>
    </div>
</template>


<script>
import { ref } from 'vue';
import { Vault } from '../models/Vault.js';
import { vaultsService } from '../services/VaultsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

export default {
    props: { vault: { type: Vault, required: true } },
    setup() {
        const editable = ref({})

        return {
            editable,
            async createVault() {
                try {
                    const formData = editable.value
                    const keep = await vaultsService.createVault(formData)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped></style>