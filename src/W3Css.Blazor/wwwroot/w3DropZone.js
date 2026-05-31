export function connect(root, input, options = {}) {
    if (!root || !input) {
        return {
            dispose() {
            }
        };
    }

    const accumulateFiles = options.accumulateFiles !== false;
    const maximumFileCount = Number.isFinite(options.maximumFileCount)
        ? Math.max(1, options.maximumFileCount)
        : 10;

    let selectedFiles = [];
    let dispatchingMergedChange = false;

    const stopBrowserFileNavigation = event => {
        event.preventDefault();
        event.stopPropagation();
    };

    const fileKey = file => `${file.name}|${file.size}|${file.lastModified}|${file.type}`;

    const normalizeFiles = files => {
        if (!files?.length) {
            return [];
        }

        if (typeof FileList !== "undefined" && !(files instanceof FileList)) {
            return [];
        }

        return Array.from(files);
    };

    const mergeFiles = incomingFiles => {
        const fileMap = new Map();
        const sourceFiles = input.multiple && accumulateFiles
            ? [...selectedFiles, ...incomingFiles]
            : incomingFiles;

        for (const file of sourceFiles) {
            const key = fileKey(file);

            if (!fileMap.has(key)) {
                fileMap.set(key, file);
            }

            if (fileMap.size >= maximumFileCount) {
                break;
            }
        }

        const files = Array.from(fileMap.values());
        return input.multiple ? files : files.slice(0, 1);
    };

    const assignFiles = files => {
        if (typeof DataTransfer !== "function") {
            return false;
        }

        const transfer = new DataTransfer();

        for (const file of files) {
            transfer.items.add(file);
        }

        input.files = transfer.files;
        return true;
    };

    const handleInputChange = () => {
        if (dispatchingMergedChange) {
            return;
        }

        const incomingFiles = normalizeFiles(input.files);

        if (!incomingFiles.length) {
            selectedFiles = [];
            return;
        }

        const files = mergeFiles(incomingFiles);

        if (assignFiles(files)) {
            selectedFiles = files;
        } else {
            selectedFiles = input.multiple ? incomingFiles.slice(0, maximumFileCount) : incomingFiles.slice(0, 1);
        }
    };

    const handleDrop = event => {
        stopBrowserFileNavigation(event);

        if (input.disabled) {
            return;
        }

        const incomingFiles = normalizeFiles(event.dataTransfer?.files);

        if (!incomingFiles.length) {
            return;
        }

        const files = mergeFiles(incomingFiles);

        if (!assignFiles(files)) {
            return;
        }

        selectedFiles = files;
        dispatchingMergedChange = true;

        try {
            input.dispatchEvent(new Event("change", { bubbles: true }));
        } finally {
            dispatchingMergedChange = false;
        }
    };

    input.addEventListener("change", handleInputChange, true);
    root.addEventListener("dragenter", stopBrowserFileNavigation);
    root.addEventListener("dragover", stopBrowserFileNavigation);
    root.addEventListener("drop", handleDrop);

    return {
        dispose() {
            input.removeEventListener("change", handleInputChange, true);
            root.removeEventListener("dragenter", stopBrowserFileNavigation);
            root.removeEventListener("dragover", stopBrowserFileNavigation);
            root.removeEventListener("drop", handleDrop);
        }
    };
}
