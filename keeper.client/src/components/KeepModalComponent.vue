<template>
    <div class="modal fade" id="modalId" tabindex="-1" aria-labelledby="modalTitleId" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-xl" role="document">
            <div class="modal-content">
                <slot></slot>
            </div>
        </div>

    </div>
</template>


<script>
export default {
    setup() {
        return {}
    }
}
</script>


<style lang="scss" scoped>
.modal-dialog {
    overflow-y: initial;
}

.modal-content {
    overflow-y: auto;
    height: calc(100vh - 122px);
}
</style>